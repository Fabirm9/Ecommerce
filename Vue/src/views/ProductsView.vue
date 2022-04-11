<template>
  <div class="row">
    <div class="col-12 mt-5">
      <h1 class="text-left">Products</h1>
      <div class="col-md-3">
        <el-button @click="drawer = true" type="primary" plain>Create</el-button>
      </div>
    </div>
    <div class="col-12 mt-5">
      
      <el-table
        :data="products"
        style="width: 100%">
        <el-table-column
          prop="id"
          label="Id"
          >
        </el-table-column>
        <el-table-column
          prop="productName"
          label="ProductName"
          >
        </el-table-column>
        <el-table-column
          prop="price"
          label="Price"
          >
        </el-table-column>
        <el-table-column          
          label="Actions"
          >
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
      title="Create product"
      :visible.sync="drawer"
      :direction="direction">
      <div class="row">
        <div class="col-md-12">
          <ProductForm/>
        </div>
      </div>
    </el-drawer>
    </div>
  </div>
</template>

<script>
import {mapActions, mapState } from 'vuex';
import ProductForm from '@/components/ProductForm.vue'
export default {
      components:{
        ProductForm
      },
      data() {
        return {
          drawer: false,
          direction: 'rtl',
        }
      },
      computed:{
        ...mapState(["products",'openModal']),
      },
      created(){
        this.$store.dispatch('getProduts');        
      },
      mounted(){
        if(this.openModal.open === true){
          let text = "edit product"
          this.handleOpenModal(text);
        }
      },
      updated(){
        if(this.openModal.open === true){
          let text = this.openModal.type == "create" ? "create product" : "deleted product"
          this.handleOpenModal(text);
        }
      },
      methods:{
        handleEdit(value){
           this.$router.push(`/product/${value}`)
        },
        handleOpenModal(text){
          this.$notify({
          title: 'Success',
          message: text,
          type: 'success'
        });
        this.openModal.open=false;
        },
        handleDeleteItem(value){
          this.$store.dispatch('deleteProduct', value);
        }
      }
      

    }
</script>