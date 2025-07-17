// CountryServices.js
//import apiClient from './apiClient.js';

const CountryServices = {
    // جلب كل الدول
    getAll: function (onSuccess, onError) {
        return apiClient.get('api/Country',onSuccess,onError,false);
    },

    // جلب دولة واحدة بالـ ID
    getById: function (id, onSuccess, onError) {
        return apiClient.get(`api/Country/${id}`, onSuccess, onError, false);
    },

    
};

//export default CountryServices;
