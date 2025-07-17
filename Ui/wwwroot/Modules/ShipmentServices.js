const DropDownHelpers = {
    fillCountryDropDown: function (selectSelector) {
        CountryServices.getAll(
            function (response) {
                console.log(response);
                console.log(selectSelector);

                $(selectSelector).empty();
                $(selectSelector).append(`<option value="">Select a Country</option>`);
                console.log(response.Data);

                response.Data.forEach(function (country) {
                    $(selectSelector).append(
                        `<option value="${country.Id}">${country.CountryAname}</option>`
                    );
                });
            },
            function (err) {
                console.error('❌ فشل تحميل الدول:', err);
                $(selectSelector).empty().append(`<option value="">فشل تحميل الدول</option>`);
            }
        );
    },

    fillShippingPackagingDropDown: function (selectSelector) {
        ShippingPackagingServices.getAll(
            function (response) {
                console.log(response);
                console.log(selectSelector);

                $(selectSelector).empty();
                $(selectSelector).append(`<option value="">Select Packaging</option>`);

                if (!response || !response.Data || !Array.isArray(response.Data)) {
                    console.warn("⚠️ البيانات غير صالحة");
                    return;
                }

                response.Data.forEach(function (item) {
                    $(selectSelector).append(
                        `<option value="${item.Id}">${item.ShipingPackgingEname}</option>`
                    );
                });
            },
            function (err) {
                console.error('❌ فشل تحميل أنواع التغليف:', err);
                $(selectSelector).empty().append(`<option value="">فشل تحميل أنواع التغليف</option>`);
            }
        );
    },

    fillShippingTypesDropDown: function (selectSelector) {
        ShippingTypesServices.getAll(
            function (response) {
                console.log(response);
                console.log(selectSelector);

                $(selectSelector).empty();
                $(selectSelector).append(`<option value="">Select Shipping Type</option>`);

                if (!response || !response.Data || !Array.isArray(response.Data)) {
                    console.warn("⚠️ البيانات غير صالحة");
                    return;
                }

                response.Data.forEach(function (type) {
                    $(selectSelector).append(
                        `<option value="${type.Id}">${type.ShippingTypeEname}</option>`
                    );
                });
            },
            function (err) {
                console.error('❌ فشل تحميل أنواع الشحن:', err);
                $(selectSelector).empty().append(`<option value="">فشل تحميل الأنواع</option>`);
            }
        );
    },

    fillCityDropDown: function (selectSelector, countryId) {
        if (!countryId) {
            console.warn("⚠️ CountryId مش متوفر");
            return;
        }

        CityServices.getByCountryId(
            countryId,
            function (response) {
                console.log(response);
                $(selectSelector).empty();
                $(selectSelector).append(`<option value="">Select a City</option>`);

                if (!response || !response.Data || !Array.isArray(response.Data)) {
                    console.warn("⚠️ البيانات غير صالحة");
                    return;
                }

                response.Data.forEach(function (city) {
                    $(selectSelector).append(
                        `<option value="${city.Id}">${city.CityEname}</option>`
                    );
                });
            },
            function (err) {
                console.error('❌ فشل تحميل المدن:', err);
                $(selectSelector).empty().append(`<option value="">فشل تحميل المدن</option>`);
            }
        );
    }
};

const ShippingService = {
    buildShippmentDTOFromForm: function () {
        const emptyGuid = "00000000-0000-0000-0000-000000000000";
        const currentUserId = localStorage.getItem("UserId") || emptyGuid;

        const shippingDate = new Date().toISOString();

        const delivryRaw = document.querySelector('input[name="delivryDate"]').value;
        const delivryDate = new Date(delivryRaw).toISOString();

        return {
            shippingDate: shippingDate,
            delivryDate: delivryDate,

            userSender: {
                userId: currentUserId,
                senderName: $('input[name="cname"]').val() || "",
                contact: $('input[name="contact"]').val() || "",
                postalCode: $('input[name="post"]').val() || "",
                email: $('input[name="email"]').val() || "",
                phone: $('input[name="tel"]').val() || "",
                cityId: $('select[name="SenderCity"]').val(),
                address: $('input[name="address1"]').val() || "",
                otherAddress: $('input[name="other-address"]').val() || "",
                isDefalte: true
            },

            userReceiver: {
                userId: currentUserId,
                receiverName: $('input[name="cname2"]').val() || "",
                contact: $('input[name="contact2"]').val() || "",
                postalCode: $('input[name="zip"]').val() || "",
                email: $('input[name="email2"]').val() || "",
                phone: $('input[name="tel2"]').val() || "",
                cityId: $('select[name="RecivarCity"]').val(),
                address: $('input[name="address4"]').val() || "",
                otherAddress: $('input[name="other-address"]').val() || "",
                isDefalte: true
            },

            shippingTypeId: $('select[name="ShipmentTypes"]').val(),
            shipingPackgingId: $('select[name="shippingPackging"]').val(),

            weight: parseFloat($('input[name="weight"]').val()) || 0,
            length: parseFloat($('input[name="length"]').val()) || 0,
            width: parseFloat($('input[name="width"]').val()) || 0,
            height: parseFloat($('input[name="height"]').val()) || 0,
            packageValue: parseFloat($('input[name="packageValue"]').val()) || 0,

            shippingRate: 0,
            trackingNumber: 0,

            paymentMethodId: null,
            userSubscriptionId: null,
            referenceId: emptyGuid
        };
    },

    saveShippingForm: function () {
        const shippingData = ShippingService.buildShippmentDTOFromForm();
        console.log("🚀 البيانات المرسلة إلى API:", shippingData);

        $.ajax({
            url: '/api/Shipment/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(shippingData),

            success: function (response) {
                console.log("✅ تم الاستلام من السيرفر:", response);
                Swal.fire({
                    title: "✅ تم إنشاء الشحنة",
                    text: "تم إرسال البيانات بنجاح!",
                    icon: "success",
                    confirmButtonText: "موافق"
                }).then(() => {
                    if (response?.data?.id || response?.Id) {
                        window.location.href = `/Shiping/Create`;
                    }
                });
            },
            error: function (xhr, status, error) {
                console.error("❌ خطأ في الطلب:", xhr.responseText);
                Swal.fire({
                    title: "❌ فشل الحفظ",
                    text: xhr.responseText || "حدث خطأ أثناء الحفظ",
                    icon: "error"
                });
            }
        });
    }
};

//function buildShippmentDTOFromForm() {
//    const emptyGuid = "00000000-0000-0000-0000-000000000000";
//    const currentUserId = localStorage.getItem("UserId") || emptyGuid;

//    const shippingDate = new Date().toISOString();

//    const delivryRaw = document.querySelector('input[name="delivryDate"]').value;
//    const delivryDate = new Date(delivryRaw).toISOString();

//    const payload = {
//        data: "Shipment info or ID", // استبدلها بالقيمة المناسبة
//        shippingDate,
//        delivryDate
//    };

//    fetch('/api/shipment/Create', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json'
//        },
//        body: JSON.stringify(payload)
//    })
//        .then(response => response.json())
//        .then(data => console.log('Success:', data))
//        .catch(error => console.error('Error:', error));

//    return {
//        shippingDate: shippingDate,
//        delivryDate: delivryDate,

//        userSender: {
//            userId: currentUserId,
//            senderName: $('input[name="cname"]').val() || "",
//            contact: $('input[name="contact"]').val() || "",
//            postalCode: $('input[name="post"]').val() || "",
//            email: $('input[name="email"]').val() || "",
//            phone: $('input[name="tel"]').val() || "",
//            cityId: $('select[name="SenderCity"]').val() ,
//            address: $('input[name="address1"]').val() || "",
//            otherAddress: $('input[name="other-address"]').val() || "",
//            isDefalte: true
//        },

//        userReceiver: {
//            userId: currentUserId,
//            receiverName: $('input[name="cname2"]').val() || "",
//            contact: $('input[name="contact2"]').val() || "",
//            postalCode: $('input[name="zip"]').val() || "",
//            email: $('input[name="email2"]').val() || "",
//            phone: $('input[name="tel2"]').val() || "",
//            cityId: $('select[name="RecivarCity"]').val() ,
//            address: $('input[name="address4"]').val() || "",
//            otherAddress: $('input[name="other-address"]').val() || "",
//            isDefalte: true
//        },

//        shippingTypeId: $('select[name="ShipmentTypes"]').val() ,
//        shipingPackgingId: $('select[name="shippingPackging"]').val() ,

//        weight: parseFloat($('input[name="weight"]').val()) || 0,
//        length: parseFloat($('input[name="length"]').val()) || 0,
//        width: parseFloat($('input[name="width"]').val()) || 0,
//        height: parseFloat($('input[name="height"]').val()) || 0,
//        packageValue: parseFloat($('input[name="packageValue"]').val()) || 0,

//        shippingRate: 0,
//        trackingNumber: 0,

//        paymentMethodId: null,
//        userSubscriptionId: null,
//        referenceId: emptyGuid
//    };
//}

//function saveShippingForm() {
//    const shippingData = buildShippmentDTOFromForm();
//    console.log("🚀 البيانات المرسلة إلى API:", shippingData);

//    $.ajax({
//        url: '/api/Shipment/Create', // تأكد إن ده نفس اسم الـ Controller بالضبط
//        type: 'POST',
//        contentType: 'application/json',
//        data: JSON.stringify(shippingData),

//        success: function (response) {
//            console.log("✅ تم الاستلام من السيرفر:", response);
//            Swal.fire({
//                title: "✅ تم إنشاء الشحنة",
//                text: "تم إرسال البيانات بنجاح!",
//                icon: "success",
//                confirmButtonText: "موافق"
//            }).then(() => {
//                if (response?.data?.id || response?.Id) {
//                    window.location.href = `/Shipment/Create`;
//                }
//            });
//        },
//        error: function (xhr, status, error) {
//            console.error("❌ خطأ في الطلب:", xhr.responseText);
//            Swal.fire({
//                title: "❌ فشل الحفظ",
//                text: xhr.responseText || "حدث خطأ أثناء الحفظ",
//                icon: "error"
//            });
//        }
//    });
//}

$(document).ready(function () {
    DropDownHelpers.fillCountryDropDown('select[name="SenderCountry"]');
    DropDownHelpers.fillCountryDropDown('select[name="RecivarCountry"]');
    DropDownHelpers.fillShippingPackagingDropDown('select[name="shippingPackging"]');
    DropDownHelpers.fillShippingTypesDropDown('select[name="ShipmentTypes"]');

    $('select[name="SenderCountry"]').on('change', function () {
        const selectedCountryId = $(this).val();
        DropDownHelpers.fillCityDropDown('select[name="SenderCity"]', selectedCountryId);
    });

    $('select[name="RecivarCountry"]').on('change', function () {
        const selectedCountryId = $(this).val();
        DropDownHelpers.fillCityDropDown('select[name="RecivarCity"]', selectedCountryId);
    });
    $(document).on('click', '#SaveShipment', function (e) {
        e.preventDefault();
       ShippingService.saveShippingForm();
    });

});


