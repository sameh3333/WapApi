
const CityServices = {
    // جلب كل المدن
    getAll: function (onSuccess, onError) {
        return apiClient.get('api/City', onSuccess, onError, false);
    },

    // جلب مدينة واحدة بالـ ID
    getById :function(id,onSuccess, onError) {
        return apiClient.get(`api/City/${id}`, onSuccess, onError, false);
    },

    // جلب مدن حسب الدولة
    getByCountryId: function (CountryId, onSuccess, onError) {
        return apiClient.get(`api/City/GetByCountry/${CountryId}`, onSuccess, onError,false);
    }
};
