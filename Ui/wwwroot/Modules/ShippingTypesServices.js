

const ShippingTypesServices = {
    // جلب كل أنواع الشحن
    getAll: function (onSuccess, onError) {
        return apiClient.get('api/ShippingTypes', onSuccess, onError, false);
    },

    // جلب نوع شحن واحد
    getById: function (id, onSuccess, onError) {
        return apiClient.get(`api/ShippingTypes/${id}`, onSuccess, onError, false);
    },

    // إنشاء نوع شحن جديد
   
};

