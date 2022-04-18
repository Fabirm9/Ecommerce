import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import router from '../router'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    products : [],
    salesProduct:[],
    clients: [],
    salesbyuser:[],
    client:'',
    identityType:'',
    identityTypes:[],
    product: {},
    saleTotal:0,
    ruleForm: {
      id:0,
      productName: '',
      price: 0,
      type:''
    },
    formClient: {
      id:0,
      firstName: '',
      lastName: '',
      idIdentityType: '',
      identityTypeNumber: '',
      mobileNumber:'',
      type:''
    },
    openModal:{
      type:'',
      open:false
    }
  },
  mutations: {
    setProducts(state,payload){
      state.products = payload;
    },
    setClients(state, payload){
      state.clients = payload
    },
    setSales(state , payload){
      state.salesProduct = payload
    },
    setClient(state, payload){
      state.client = payload;
    },
    setTotal(state , payload){
      state.saleTotal = payload;
    },
    setProduct(state, payload){
      state.product = payload;
    },
    setFormProduct(state, payload){
      state.ruleForm = payload;
    },
    setOpenModal(state, payload){
      state.openModal = payload;
    },
    setIdentityTypes(state, payload){
      state.identityTypes = payload;
    }
  },
  actions: {
    getProduts : async function({commit}){
      try {
        await axios.get('http://localhost:10000/api/ecommerceproducts').then(response => {
        const ArrayProducts = response.data.data.map(item =>{
          return {
             id : item.id,
             productName: item.productName,
             quantity:1,
             price: item.price,
             total:0
          }
        });
        commit('setProducts', ArrayProducts);
        })        
      } catch (error) {
        console.log(error);
      }
    },
    getClients : async function({commit}){
      try {
        await axios.get('http://localhost:10000/api/ecommerceclients').then(response =>{
          commit('setClients', response.data.data);
        })
      } catch (error) {
        console.log(error);
      }
    },
    postSales : async function({commit}, data){
      try {
        commit('setSales', data);        
        let payload = {
          saleViewModel: data
        };
        
        await axios.post('http://localhost:10000/api/ecommercesale',payload,
          {
          headers: {
          'Content-Type': 'application/json'
          }
        }).then(response => {
          
          const dataModal = {type:'create', open:true};
          commit('setSales', []);
          commit('setTotal', 0);
          commit('setClient', '');
          commit('setOpenModal', dataModal);
          
        })
      } catch (error) {
        console.log(error);
      }
    },
    createProduct: async function({commit, state}, data){
      try {
        commit('setProduct', data);             
        
        await axios.post('http://localhost:10000/api/ecommerceproducts',data,
          {
          headers: {
          'Content-Type': 'application/json'
          }
        }).then(response => {          
            if(response.status === 200){
              let dataModal = {type:'create', open:true};              
              commit('setOpenModal', dataModal);
              state.ruleForm ={
                id:0,
                productName: '',
                price: 0,
                type:''
              }
              state.products.push({
                id: response.data,
                productName: data.productName,
                price : data.price
              });
            }
        });
      } catch (error) {
        console.log(error);
      }
    },
    editProduct: async function({commit, state}, data){
      try {
        commit('setProduct', data);             
        
        await axios.put(`http://localhost:10000/api/ecommerceproducts/${data.id}`,data,
          {
          headers: {
          'Content-Type': 'application/json'
          }
        }).then(response => {          
            if(response.status === 200){
              let dataModal = {type:'edit', open:true};              
              router.push('/products');
              commit('setOpenModal', dataModal);
              state.ruleForm ={
                id:0,
                productName: '',
                price: 0,
                type:''
              }
            }
        });
      } catch (error) {
        console.log(error);
      }
    },
    deleteProduct: async function({commit,  state }, data){      
      try {    
        await axios.delete(`http://localhost:10000/api/ecommerceproducts/${data}`,
          {
          headers: {
          'Content-Type': 'application/json'
          }
        }).then(response => {          
            if(response.status === 200){
              let dataModal = {type:'delete', open:true};
              commit('setOpenModal', dataModal);
              let products = state.products;
              let newData = products.filter(item => item.id !== parseInt(data));
              commit('setProducts', newData);
            }
        });
      } catch (error) {
        console.log(error);
      }
    },
    getIdentityTypes: async function({commit, state}, data){
      try {
        await axios.get('http://localhost:10000/api/ecommerceidentitytypes').then(response =>{
          commit('setIdentityTypes', response.data.data);
        })
      } catch (error) {
        console.log(error);
      }
    },
    createClient: async function({commit, state}, data){
      try {
        await axios.post('http://localhost:10000/api/ecommerceclients',data,
          {
          headers: {
          'Content-Type': 'application/json'
          }
        }).then(response => {          
            if(response.status === 200){
              let dataModal = {type:'create', open:true};              
              commit('setOpenModal', dataModal);
              state.formClient ={
                id:0,
                firstName: '',
                lastName: '',
                idIdentityType: '',
                identityTypeNumber: '',
                mobileNumber:'',
                type:''
              }
              state.clients.push({
                id: response.data,
                firstName: data.firstName,
                lastName : data.lastName,
                mobileNumber: data.mobileNumber,
                idIdentityType: data.idIdentityType,
                identityTypeNumber: data.identityTypeNumber
              });
              commit('setClient','')
            }
        });
      } catch (error) {
        console.log(error);
      }
    },
    deleteClient: async function({commit,  state }, data){      
      try {    
        await axios.delete(`http://localhost:10000/api/ecommerceclients/${data}`,
          {
          headers: {
          'Content-Type': 'application/json'
          }
        }).then(response => {          
            if(response.status === 200){
              let dataModal = {type:'delete', open:true};
              commit('setOpenModal', dataModal);
              let clients = state.clients;
              let newData = clients.filter(item => item.id !== parseInt(data));
              commit('setClients', newData);
            }
        });
      } catch (error) {
        console.log(error);
      }
    },
  },
  modules: {
  }
})
