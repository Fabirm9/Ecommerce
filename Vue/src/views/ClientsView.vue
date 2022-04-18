<template>
     <div class="row">
    <div class="col-12 mt-5">
      <h1 class="text-left">Clients</h1>
      <div class="col-md-3">
        <el-button @click="drawer = true" type="primary" plain>Create</el-button>
      </div>
    </div>
    <div class="col-12 mt-5">      
      <el-table
        :data="clients"
        style="width: 100%">
        <el-table-column
          prop="id"
          label="Id">
        </el-table-column>
        <el-table-column
          prop="firstName"
          label="FirstName">
        </el-table-column>
        <el-table-column
          prop="lastName"
          label="LastName">
        </el-table-column>
        <el-table-column
          prop="mobileNumber"
          label="MobileNumber">
        </el-table-column>
        <el-table-column
          prop="identityTypeName"
          label="IdentityTypeName">
        </el-table-column>
        <el-table-column
          prop="identityTypeNumber"
          label="IdentityTypeNumber">
        </el-table-column>
        <el-table-column          
          label="Actions">
          <template slot-scope="scope">
        <el-button type="primary" icon="el-icon-edit" circle @click="handleEdit(`${scope.row.id}`)">
           </el-button>           
           <el-button type="danger" icon="el-icon-delete" circle @click="handleDeleteItem(`${scope.row.id}`)"></el-button>
            </template>
        </el-table-column>
        
      </el-table>
      
    </div>
    <div>
    <el-drawer
      title="Create client"
      :visible.sync="drawer"
      :direction="direction">
      <div class="row">
        <div class="col-md-12">
          <ClientForm/>
        </div>
      </div>
    </el-drawer>
    </div>
  </div>
</template>
<script>
import {mapActions, mapState } from 'vuex';
import ClientForm from '@/components/ClientForm.vue'
export default {
    components:{
      ClientForm      
    },
    data(){
        return{
            drawer:false,
            direction:'rtl'
        }

    },
    computed:{
        ...mapState(['clients'])
    },
    mounted(){
        this.$store.dispatch('getClients','openModal');
    },
    updated(){
      if(this.$store.state.openModal.open === true){
          let text = this.$store.state.openModal.type == "create" ? "create product" : "deleted product"
          this.handleOpenModal(text);
        }
    },
    methods:{
      handleOpenModal(text){
          this.drawer=false;
          this.$notify({
          title: 'Success',
          message: text,
          type: 'success'
        });
        this.$store.state.openModal={
          open:false,
          type:''
        };
      },
      handleEdit(value){
        this.$store.dispatch('deleteCient', value);
      },
      handleDeleteItem(value){
        this.$store.dispatch('deleteClient', value);
      }
    }

}
</script>