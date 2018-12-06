import equals from './equals'
import api from '../../api/books'

const state = {
    items: []
}

const mutations = {
    addBook (state, book) {
        if(!state.items.find(x => equals(x, book)))
            state.items.push({...book});
    },
    addBooks (state, books) {
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
            const books = await api.getAllBooks();
            commit('addBooks', books);
        }
        catch(err) {
            // eslint-disable-next-line 
            console.log('nie polaczylem sie\n' + err);
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