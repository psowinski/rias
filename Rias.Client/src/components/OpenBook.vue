<template>
  <div>
    <h3>Open new book</h3>
    <p>Book name: <input type="text" v-model="book.name"/></p>
    <p>Book start date: <input type="date"  v-model="book.date"/></p>
    <p><button @click="openBook">Open</button></p>
  </div>
</template>

<script>
export default {
  name: 'OpenBook',
  data () { return {
      book: {
        name: "",
        date: new Date().toISOString().substring(0, 10)
  }}},
  methods: {
      openBook () {
          this.$store.dispatch('books/openBook', this.book)
      }
  }
}
</script>

<style scoped>
div {
    border: 1px solid grey;
    text-align: left;
}
p {
  margin: 5px 5px;
}
</style>
