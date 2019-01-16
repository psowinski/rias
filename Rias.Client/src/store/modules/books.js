import equals from './equals'
import api from '../../api/books'

const state = {
    items: []
}

const mutations = {
    addBook (state, book) {
        state.items.push({...book});
    },
    setBooks (state, books) {
        state.items = books;
    }
}

const getters = {
    names: (state) => {
        return state.items.map(({name, date}) => name + ' - ' + date);
    }
}

const actions = {
    async getAllBooks({commit}) {
        try {
            const data = await api.getAllBooks();
            commit('setBooks', data.books);
        }
        catch(err) {
            // eslint-disable-next-line
            console.log('getAllBooks faild: ' + err);
        }
    },

    async openBook({commit, state}, book) {
        if(!state.items.find(x => equals(x, book)))
        {
            try {
                await api.openBook(book);
                commit('addBook', book);
            }
            catch(err) {
                // eslint-disable-next-line
                console.log('openBook faild: ' + err);
            }
        }
    }
}

export default {
    namespaced: true,
    state,
    getters,
    mutations,
    actions
}