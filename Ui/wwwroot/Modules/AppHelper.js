window.AppHelper = {
    getCookie: function (name) {
        const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
        return match ? match[2] : null;
    },
    setCookie: function (name, value, minutes = 30) {
        const d = new Date();
        d.setTime(d.getTime() + (minutes * 60 * 1000));
        document.cookie = `${name}=${value};expires=${d.toUTCString()};path=/`;
    },
    refreshToken: function (callback) {
        const refreshToken = AppHelper.getCookie('refreshToken');

        $.ajax({
            url: 'https://localhost:7034/api/Auth/refresh-token', // غيّره حسب مسارك
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ refreshToken: refreshToken })
        })
            .done(function (response) {
                AppHelper.setCookie('accessToken', response.accessToken, 30);
                if (callback) callback();
            })
            .fail(function () {
                alert('فشل تحديث التوكن. الرجاء تسجيل الدخول من جديد.');
                window.location.href = '/login'; // تعديل حسب المسار عندك
            });
    }
};
