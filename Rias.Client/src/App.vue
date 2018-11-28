<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png" id="logo">
    <OpenBook/>
    <BooksList/>
  </div>
</template>

<script>
import OpenBook from './components/OpenBook.vue'
import BooksList from './components/BooksList.vue'

export default {
  name: 'app',
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

#logo {
  width:25px;
  height:25px;
}
</style>
