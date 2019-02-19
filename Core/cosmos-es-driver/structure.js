import { storeEventDefinition } from './procedures'

const ensureDatabase = async function(client, name) {
    const { database } = await client.databases.createIfNotExists({
        id: name
    });
    return database;
}

const ensureContainer = async function(database, name) {
    const { container } = await database.containers.createIfNotExists({
        id: name
    });
    return container;
}

const ensureProcedure = async function(collection, procedure) {
    const { sproc } = await collection.storedProcedures.upsert(procedure);
    return sproc;
}

export const ensureStructure = async function(client, dbName) {
    const database = await ensureDatabase(client, dbName);
    const events = await ensureContainer(database, 'Events');
    const storeEvent = await ensureProcedure(events, storeEventDefinition);
    const snapshots = await ensureContainer(database, 'Snapshots');
    const projections = await ensureContainer(database, 'Projections');
    return {
        store: async x => await storeEvent.execute(x),
        load: async function (streamId, from, to) {

            let q = {
                query: "SELECT * FROM c WHERE c.id<>@head and c.streamId=@streamId",
                parameters: [{
                    name: "@head",
                    value: streamId + "|head"
                },{
                    name: "@streamId",
                    value: streamId
                }]
            };

            if(from) {
                q.query += " and c.version>=@from";
                q.parameters.push({
                    name: "@from",
                    value: from
                  });
            }

            if(to) {
                q.query += " and c.version<=@to";
                q.parameters.push({
                    name: "@to",
                    value: to
                  });
            }            
            const { result } = await events.items.query(q).toArray();
            return result;
        },
        containers: {
            database,
            events,
            snapshots,
            projections }
    }
}
