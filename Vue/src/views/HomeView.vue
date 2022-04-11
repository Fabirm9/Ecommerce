<template>
  <div class="col-md-12 text-center" >
    
    <HelloWorld msg="Welcome to Ecommerce"/>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'

export default {
  name: 'HomeView',
  components: {
    HelloWorld
  }
}
</script>
