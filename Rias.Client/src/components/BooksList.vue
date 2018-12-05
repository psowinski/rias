<template>
  <div>
    <h3><img alt="Vue logo" src="../assets/logo.png">Books list</h3>
    <ol>
        <li v-for="book in books" :key="book">
        {{ book }}
        </li>
    </ol>

  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'BooksList',
  computed: {
    ...mapGetters({
       books: 'books/names'
    })
  }
}
</script>

<style scoped>
div {
    border: 1px solid grey;
    text-align: left;
}
li {
  margin: 10px 0px;
}

img {
  width:25px;
  height:25px;
}
</style>
