<template>
<div class="row">
    <div class="col-6 mt-5">
      <h1 class="text-start">Sales</h1>
    </div>
    <div class="col-6 mt-5 text-end">
      <el-button v-if="salesProduct.length > 0" @click="drawer = true"><i class="el-icon-shopping-cart-full"> #items: {{salesProduct.length}}</i></el-button>
    </div>
    <div class="card col-md-6 mb-2" v-for="(item, index) in products">
        <!-- <img src="..." class="card-img-top" alt="..."> -->
        <div class="row">
        <div class="card-body col-md-6">
            <p class="card-text">{{item.productName}}</p>
            <span>price : ${{item.price}}</span>
        </div>
        <div class="col-md-6">
            <div class="text-center py-2"><el-input-number v-model="item.quantity" :min="1" :max="10"></el-input-number></div>
            <div class="text-center py-2"><el-button @click="handleBuy(item)" type="success" round>Add To Car</el-button></div>
        </div>
        </div>       
    </div>
    <el-drawer
        title="I am the title"
        :visible.sync="drawer"
        :show-close="true"
        :with-header="false">
        <div class="row">
            <div class="col-md-12">
            <p class="mb-4">CAR --- Buy Elements</p>
            <div class="row my-2">
                <div class="col-md-4 ps-2 pt-2">Select Client:</div>
                <div class="col-md-8">
                    <el-select class="w-100" v-model="$store.state.client" @change="handleChangeSelect" placeholder="Select">
                        <el-option
                            v-for="item in clients"
                            :key="item.id"
                            :label="item.firstName + ' ' + item.lastName"
                            :value="item.id">
                        </el-option>
                    </el-select>
                </div>
            </div>
            <div class="card" v-for="(item, index) in salesProduct">
                <ul class="list-group list-group-flush">
                    <li class="list-group-item">                        
                        <span class="text-start">{{item.productName}}</span> x {{item.quantity}} <span class="text-end">${{item.total}}</span>
                        <div class="col-md-12 text-end"><el-button @click="handleDeleteItem(item)" size="mini" type="danger" icon="el-icon-delete" circle></el-button></div>
                    </li>                    
                </ul>                
            </div>
            <div class="col-md-12">
                <p class="pt-2 pe-4 text-end">Total $ {{saleTotal}}</p>                
            </div>
            <div class="row">            
                <div class="col-md-6">
                    <el-button class="ps-4" type="text" @click="drawer = false" >Cancel Buy</el-button>
                </div>
                <div class="col-md-6">
                    <el-button type="success" @click="handleSendData()">Buy Now <i class="el-icon-shopping-cart-1"></i></el-button>
                </div>
            </div>
            </div>
        </div>
    </el-drawer>
</div>
</template>

<script>
import { mapState } from 'vuex';
export default {
      data() {
        return {            
            drawer: false,
            valueSelect : 0,
        }
      },
      computed:{
        ...mapState(["products","salesProduct","saleTotal","clients", "client",'openModal']),
      },
      created(){
        this.$store.dispatch('getProduts')
        this.$store.dispatch('getClients')
      },
      updated(){
          if(this.openModal.open === true)
          {
              this.handleOpenModal('Buy created');
          }
      },
      methods:{
        handleBuy(item) {
            this.drawer = true;
            var total = item.price * item.quantity;
            this.salesProduct.push({
                idProduct: item.id,
                productName: item.productName,
                unitPrice : item.price,
                quantity : item.quantity,
                total :total,
                idClient:0
            });     
            this.$store.state.saleTotal =0;       
            for(var i=0; i<this.salesProduct.length; i++)
            {                
                this.$store.state.saleTotal +=this.salesProduct[i].total; 
            }            
      },
      handleChangeSelect(value){
        this.valueSelect = value;
      },
      handleDeleteItem(item){
            for(var i=0; i<this.salesProduct.length; i++)
            {
                if(this.salesProduct[i].idProduct == item.idProduct){
                    this.$store.state.saleTotal -=item.total;
                } 
            }   
            this.$store.state.salesProduct = this.salesProduct.filter(saleProduct => saleProduct.idProduct !== item.idProduct);
      },
      handleSendData(){          
          for(var i=0; i < this.$store.state.salesProduct.length; i++){
              this.$store.state.salesProduct[i].idClient = this.valueSelect;
          }
          this.$store.dispatch('postSales', this.$store.state.salesProduct);
      },
      handleOpenModal(text){
          this.$notify({
          title: text,
          message: text,
          type: 'success'
        });
        this.drawer = false;
        this.$store.state.openModal=false;
       }
    }
}
</script>

<style scoped>
.row{
--bs-gutter-x : 0rem !important;
}
</style>