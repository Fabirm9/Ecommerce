<template>
    <div>
    <div class="row">
    <div class="col-md-12">
    <el-form :model="formClient" :rules="rules" ref="formClient" label-width="120px" class="demo-ruleForm">
        <el-form-item label="FirstName" prop="firstName">
            <el-input v-model="formClient.firstName"></el-input>            
        </el-form-item>
        <el-form-item label="LastName" prop="lastName">
            <el-input v-model="formClient.lastName"></el-input>            
        </el-form-item>
        <el-form-item label="IdentityType" prop="idIdentityType">
            <el-select class="w-100" v-model="$store.state.formClient.idIdentityType" placeholder="Select">
                <el-option
                    v-for="item in identityTypes"
                    :key="item.id"
                    :label="item.identityTypeName"
                    :value="item.id">
                </el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="IdentityTypeNumber" prop="identityTypeNumber">            
            <el-input v-model="formClient.identityTypeNumber"></el-input>
        </el-form-item>
        <el-form-item label="MobileNumber" prop="mobileNumber">            
            <el-input v-model="formClient.mobileNumber"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="submitForm('formClient')">Save</el-button>
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
        return{
            rules: {
                firstName: [
                    { required: true, message: 'Please input first name', trigger: 'blur' },
                    { min: 3, max: 20, message: 'Length should be 3 to 20', trigger: 'blur' }
                ],
                lastName: [
                    { required: true, message: 'Please input last name', trigger: 'blur' },
                    { min: 3, max: 20, message: 'Length should be 3 to 20', trigger: 'blur' }
                ],
                mobileNumber:[
                    { required: true, message: 'Please input mobile number', trigger: 'blur' },
                    { min: 7, max: 10, message: 'Length should be 7 to 10', trigger: 'blur' }
                ],
                identityTypeNumber:[
                    { required: true, message: 'Please input identityTypeNumber', trigger: 'blur' },
                    { min: 3, max: 12, message: 'Length should be 3 to 12', trigger: 'blur' }
                ],
                idIdentityType:[
                    { required: true, message: 'Please select input identityType', trigger: 'blur' }
                ]        
            }
        }
    },
    computed:{
        ...mapState(['formClient','identityType','identityTypes'])
    },
    mounted(){
        this.$store.dispatch('getIdentityTypes');
    },
    methods:{
        submitForm(formName) {
        this.$refs[formName].validate((valid) => {
          if (valid) {
              if(this.formClient.type== 'edit'){
                this.formClient.id = this.$route.params.id;
                this.$store.dispatch('editProduct',this.formClient);
              }
              else{
                this.$store.dispatch('createClient',this.formClient);
              }
          } else {
            console.log('error submit!!');
            return false;
          }
        });
        },
    }
}
</script>