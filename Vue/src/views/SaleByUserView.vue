<template>
    <div class="col-md-12">
        <el-select class="w-100" v-model="$store.state.client" @change="handleIdUser" placeholder="Select">
                        <el-option
                            v-for="item in clients"
                            :key="item.id"
                            :label="item.firstName + ' ' + item.lastName"
                            :value="item.id">
                        </el-option>
        </el-select>
        <div></div>
    <el-table
        :data="salesbyuser"
        style="width: 100%">
        <el-table-column
          prop="idClient"
          label="IdClient"
          >
        </el-table-column>
        <el-table-column
          prop="fullName"
          label="FullName"
          >
        </el-table-column>
        <el-table-column
          prop="idProductName"
          label="IdProductName"
          >
        </el-table-column>
        <el-table-column
          prop="productName"
          label="ProductName"
          >
        </el-table-column>
        <el-table-column
          prop="quantity"
          label="Quantity"
          >
        </el-table-column>
        <el-table-column
          prop="unitPrice"
          label="UnitPrice"
          >
        </el-table-column>      
        <el-table-column
          prop="total"
          label="Total"
          >
        </el-table-column>    
        
      </el-table>
    </div>
</template>

<script>
import { mapState } from 'vuex';
import axios from 'axios'
export default {
    data(){
        return {
            
        }
    },
    computed:{
        ...mapState(['clients','salesbyuser'])
    },
    mounted(){
        this.$store.dispatch('getClients');
    },
    methods:{
        handleIdUser(id){
            axios.get(`http://localhost:10000/api/ecommercesale/${id}`)
            .then(response => {
                if(response.status === 200){
                  this.$store.state.salesbyuser = response.data.data;   
                }
            })
        }
    }
    
}
</script>