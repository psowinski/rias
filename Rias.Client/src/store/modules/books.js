import equals from './equals'

const state = {
    items: [
        {name: "Book 1", date: "2018-01-01"},
        {name: "Book 2", date: "2018-05-05"},
        {name: "Book 3", date: "2018-08-08"}
    ]
}
const mutations = {
    addBook (state, book) {
        if(!state.items.find(x => equals(x, book)))
            state.items.push({...book});
    }
}

const getters = {
    names: (state) => {
        return state.items.map(({name, date}) => name + ' - ' + date);
    }
}

export default {
    namespaced: true,
    state,
    getters,
    mutations
}