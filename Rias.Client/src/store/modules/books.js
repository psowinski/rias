const state = {
    books: [
        {name: "Book 1"},
        {name: "Book 2"},
        {name: "Book 3"}
    ]
}

const mutations = {
    openBook (state, { name }) {
      state.books.push(name);
    }
}

export default {
    namespaced: true,
    state,
    mutations
}