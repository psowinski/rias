module.exports = async function (context, req) {
    context.log('Open book req:' + JSON.stringify(req.body));
};