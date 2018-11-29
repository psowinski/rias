<template>
  <div id="app">
    <OpenBook class="component" v-on:bookOpen="onBookOpen"/>
    <BooksList class="component" v-bind:books="books"/>
  </div>
</template>

<script>
import OpenBook from './components/OpenBook.vue'
import BooksList from './components/BooksList.vue'

export default {
  name: 'app',
  data () { return {
      books: [
          "Book 1",
          "Book 2",
          "Book 3"
      ]
  }},
  methods: {
    onBookOpen: function (book) {
      this.books.push(book.name);
    }
  },
  components: {
    OpenBook, BooksList
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

.component {
  margin: 5px;
}
</style>
