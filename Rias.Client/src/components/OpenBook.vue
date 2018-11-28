<template>
  <div>
    <h3 v-if="seen">Open new book</h3>
    <p>Book name: <input type="text" /></p>
    <p>Book start date: <input type="text" /></p>
    <p><button>Open</button></p>
  </div>
</template>

<script>
export default {
  name: 'OpenBook',
  data () { return {
      seen: true
  }}
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
