module.exports = async function (context, req) {
    //context.log('HTTP trigger: getAllBooks.');

    return {
        //headers: { 'Content-Type': 'application/json' },
        body: { "books": [
            { "name": "Book A1", "date": "2018-10-01" },
            { "name": "Book F2", "date": "2018-10-02" },
            { "name": "Book F3", "date": "2018-10-03" }
        ]}
    };
};