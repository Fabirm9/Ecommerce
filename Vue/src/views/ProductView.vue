<template>
<div>
    <ProductForm/>
</div>
</template>

<script>
import axios from 'axios'
import {mapActions, mapState } from 'vuex';
import ProductForm from '@/components/ProductForm.vue'
export default {
    components:{
        ProductForm
    },
    data(){
        return {
            rules: {
                productName: [
                    { required: true, message: 'Please input product name', trigger: 'blur' },
                    { min: 3, max: 20, message: 'Length should be 3 to 20', trigger: 'blur' }
                ],
                price: [
                    { required: true, message: 'Please input product price', trigger: 'blur' }
                ]          
            }
        }
    },
    computed:{
        ...mapState(['ruleForm']),
    },
    mounted(){
        this.ruleForm.type='edit';       
        axios.get(`http://localhost:10000/api/ecommerceproducts/${this.$route.params.id}`)
        .then(response => {
            if(response.status === 200){
                this.ruleForm.productName = response.data.productName;
                this.ruleForm.price = response.data.price;
            }
        })
    }
    

}
</script>