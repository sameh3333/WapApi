

const ShippingPackagingServices = {
    // جلب كل أنواع التغليف
    getAll: function (onSuccess, onError) {
        return apiClient.get('api/ShippingPackging', onSuccess, onError, false);
    },

    // جلب نوع تغليف بالـ ID
    getById: function (id, onSuccess, onError) {
        return apiClient.get(`api/ShippingPackging/${id}`, onSuccess, onError, false);
    },

   
};


