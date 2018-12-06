import axios from 'axios'
import apiurl from './apiurl'

export default {
    async getAllBooks() {
        const addr = apiurl('books');
        const response = await axios.get(addr);
        return response.data;
    },

    async openBook(book) {
        await axios.post(apiurl('books/open'), JSON.stringify(book));
    }
}
