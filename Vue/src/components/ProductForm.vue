<template>
<div>
    <h1>Product</h1>
    <div class="row">
    <div class="col-md-12">
    <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="120px" class="demo-ruleForm">
        <el-form-item label="Product Name" prop="productName">
            <el-input v-model="ruleForm.productName"></el-input>            
        </el-form-item>
        <el-form-item label="Price" prop="price">            
            <el-input-number v-model="ruleForm.price" :min="1" :max="100000000"></el-input-number>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="submitForm('ruleForm')">Save</el-button>
            <el-button class="ps-4" type="text" @click="cancelTransation()" >Cancel</el-button>            
        </el-form-item>
    </el-form>
    </div>
    </div>
</div>
</template>
<script>
import { mapState } from 'vuex';
export default {
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
        ...mapState(["ruleForm"]),
    },
    methods:{
        submitForm(formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
              if(this.ruleForm.type== 'edit'){
                this.ruleForm.id = this.$route.params.id;
                this.$store.dispatch('editProduct',this.ruleForm);
              }
              else{
                this.$store.dispatch('createProduct',this.ruleForm);
              }
          } else {
            console.log('error submit!!');
            return false;
          }
        });
        },
        cancelTransation(){
            this.$store.state.ruleForm ={
                id:0,
                productName: '',
                price: 0,
                type:''
              }
              this.$router.push('/products');
        }
    }
}
</script>