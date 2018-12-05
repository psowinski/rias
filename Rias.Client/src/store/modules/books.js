const state = {
    items: [
        {name: "Book 1", date: "2018-01-01"},
        {name: "Book 2", date: "2018-05-05"},
        {name: "Book 3", date: "2018-08-08"}
    ]
}

const mutations = {
    openBook (state, { name }) {
      state.books.push(name);
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