﻿let alerte = {
    ConfirmDelete: function (callBack) {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                callBack(result); 
               
            }
        });
    },
    Success: function (title, text) {
        Swal.fire({
            title: title,
            text: text,
            icon: "success"
        });
    },
    Error: function (title, text) {
        Swal.fire({
            title: title,
            text: text,
            icon: "error"
        });
    }
};

//let alerte = {
//    ConfirmDelete: function (callback) {
//        Swal.fire({
//            title: "Are you sure?",
//            text: "You won't be able to revert this!",
//            icon: "warning",
//            showCancelButton: true,
//            confirmButtonColor: "#3085d6",
//            cancelButtonColor: "#d33",
//            confirmButtonText: "Yes, delete it!"
//        }).then((result) => {
//            if (result.isConfirmed) {
//                console.log("Callback function:", callback);
//                if (typeof callback === "function") {


//                    callback(result);

//                    Swal.fire({
//                        title: title,
//                        text: text,
//                        icon: "success"
//                    });

//                } else {
//                    console.error("Error: callback is not a function");
//                }
//            }
//        });
//    },
//    Success: function (title, text) {
//        Swal.fire({
//            title: title,
//            text: text,
//            icon: "success"
//        });
//    },
//    Error: function (title, text) {
//        Swal.fire({
//            title: title,
//            text: text,
//            icon: "error"
//        });
//    }
//};
