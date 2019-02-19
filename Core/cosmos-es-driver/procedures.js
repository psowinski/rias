export const storeEventDefinition = {
    id: 'storeEvent',
    body: function run(evn) {
        if(typeof evn === 'string')
            evn = JSON.parse(data);

        var container = getContext().getCollection();

        requestStream();

        function requestStream() {
            var query = 'SELECT * FROM c where c.id  = "' + evn.streamId + '|head"';
            var accept = container.queryDocuments(container.getSelfLink(), query, {}, onStreamReceive);
            if (!accept) throw new Error('Get head query was not accept by the server.');
        }

        function onStreamReceive(err, docs, opt) {
            checkError(err);
            var head = moveHead(docs);
            processEvent(head);
        }

        function checkError(err) {
            if (err) throw new Error(err);
        }

        function moveHead(documents) {
            var head = fetchHead(documents);
            return head ? incrementVersion(head) : createHead();
        }

        function fetchHead(documents) {
            return documents && documents.length === 1 ? documents[0] : null;
        }

        function incrementVersion(item) {
            item.version = item.version + 1;
            return item;
        }

        function createHead() {
            return {
                id: evn.streamId + "|head",
                streamId: evn.streamId,
                version: 1
            };        
        }

        function processEvent(head) {
            checkAndSetEventVersion(head.version);
            setIdentity();
            storeDocument(evn);
            storeDocument(head);
        }

        function checkAndSetEventVersion(version) {
            if(!evn.version) {
                evn.version = version;
            }
            else if(evn.version !== version) {
                throw new Error('[VersionConflict] Incorrect event version, expected ' + version + ' is ' + evn.version);
            }
        }

        function setIdentity() {
            evn.id = evn.streamId + "|" + evn.version;
        }

        function storeDocument(document) {
            if(!document._self) {
                createDocument(document);
            } else {
                replaceDocument(document);
            }
        }

        function createDocument(document) {
            var accept = container.createDocument(container.getSelfLink(), document, function (err, doc) { checkError(err); });
            if (!accept) throw new Error('Create query was not accept by the server.');
        }

        function replaceDocument(document) {
            var accept = container.replaceDocument(document._self, document, function (err, doc) { checkError(err); });
            if (!accept) { throw new Error("Replace query was not accept by the server."); }
        }
    }
}
