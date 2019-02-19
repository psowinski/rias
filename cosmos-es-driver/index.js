import { CosmosClient } from '@azure/cosmos'
import { config } from './config'
import { ensureStructure } from './structure'

export const connect = async function(dbName) {
    const client = new CosmosClient({
        endpoint: config.endpoint,
        auth: {
            masterKey: config.key
        }
    });

    const structure = await ensureStructure(client, dbName);
    return structure;
}
