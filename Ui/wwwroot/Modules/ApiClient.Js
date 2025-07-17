window.apiClient = {
    baseURL: 'https://localhost:7225/',

    get(endpoint, onSuccess, onError, useAuth = true) {
        const token = AppHelper.getCookie('accessToken');
        $.ajax({
            url: this.baseURL + endpoint,
            method: 'GET',
            headers: useAuth ? { 'Authorization': `Bearer ${token}` } : {}
        })
            .done(onSuccess)
            .fail(function (jqXHR) {
                if (jqXHR.status === 401 && useAuth) {
                    // محاولة تحديث التوكن
                    AppHelper.refreshToken(function () {
                        // أعد المحاولة بعد تحديث التوكن
                        apiClient.get(endpoint, onSuccess, onError, useAuth);
                    });
                } else {
                    if (onError) onError(jqXHR);
                }
            });
    },

    post(endpoint, data, onSuccess, onError, useAuth = true) {
        const token = AppHelper.getCookie('accessToken');
        const isFormData = data instanceof FormData;

        $.ajax({
            url: this.baseURL + endpoint,
            method: 'POST',
            contentType: isFormData ? false : 'application/json',
            processData: !isFormData,
            data: isFormData ? data : JSON.stringify(data),
            headers: useAuth ? { 'Authorization': `Bearer ${token}` } : {}
        })
            .done(onSuccess)
            .fail(function (jqXHR) {
                if (jqXHR.status === 401 && useAuth) {
                    AppHelper.refreshToken(function () {
                        apiClient.post(endpoint, data, onSuccess, onError, useAuth);
                    });
                } else {
                    if (onError) onError(jqXHR);
                }
            });
    }
};
