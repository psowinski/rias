<template>
  <div>
    <h3>Books list</h3>
    <ol>
        <li v-for="book in books" :key="book">
        {{ book }}
        </li>
    </ol>

  </div>
</template>

<script>
export default {
  name: 'BooksList',
  data() { return {
      books: [
          "Book 1",
          "Book 2",
          "Book 3"
      ]
  }}
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
</style>
