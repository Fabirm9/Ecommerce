<template>
  <div class="container">
    <div class="row">
      <div class="col-12">
        {{msg}}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  }
}
</script>
