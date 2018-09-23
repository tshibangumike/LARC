if (typeof (larc) == "undefined") {
    larc = {};
};

larc.Utilities = {
    GetHtmlTemplateRootFolderName: function () {
        return "/Components/"
    },
    GetComponentHtmlTemplateUrl: function (ComponentName, routeName) {
        return larc.Utilities.GetHtmlTemplateRootFolderName() + ComponentName + "/" + routeName + ".html";
    }
}

larc.Functions = {
    SwapArrayElements: function (array, x, y) {
        if (a.length === 1) return a;
        a.splice(y, 1, a.splice(x, 1, a[y])[0]);
        return a;
    },
    GetMonthFromDate: function ($filter, date, format) {
        return $filter("date")(parseInt(date.substr(6)), "MMMM");
    }
};

larc.Entities = {
    SideMenuGroup: {
        EntityName: "SideMenuGroup",
        Urls: {
            GetActiveDeceaseds: "/Deceased/GetActiveDeceaseds",
            GetDeceasedById: "/Deceased/GetDeceasedById",
            GetDeceasedByFuneralId: "/Deceased/GetDeceasedByFuneralId",
            UpdateDeceased: "/Deceased/UpdateDeceased",
            DeactivateDeceased: "/Deceased/DeactivateDeceased"
        }
    }
};

larc.Notify = {
    Toastr: {
        SetToastrOption: function () {
            toastr.options = {
                "debug": false,
                "positionClass": "toast-top-center",
                "progressBar": true,
                "onclick": null,
                "fadeIn": 300,
                "fadeOut": 1000,
                "timeOut": 5000,
                "extendedTimeOut": 1000
            }
        },
        CreateSuccessNotification: function (message) {
            larc.Notify.Toastr.SetToastrOption();
            if (_.isNull(message) || _.isUndefined(message)) {
                message = "The record has been created successfully!";
            }
            toastr.success(message, "Success");
        },
        CreateErrorNotification: function (message) {
            larc.Notify.Toastr.SetToastrOption();
            if (_.isNull(message) || _.isUndefined(message)) {
                message = "An error has occured while creating this record!";
            }
            toastr.error(message, "Error");
        },
        UpdateSuccessNotification: function (message) {
            larc.Notify.Toastr.SetToastrOption();
            if (_.isNull(message) || _.isUndefined(message)) {
                message = "The record has been successfully modified!";
            }
            toastr.success(message, "Success");
        },
        UpdateErrorNotification: function (message) {
            larc.Notify.Toastr.SetToastrOption();
            if (_.isNull(message) || _.isUndefined(message)) {
                message = "An error has occured while saving this record!";
            }
            toastr.error(message, "Error");
        },
    }
};

larc.Loading = {
    Start: function (message) {
        if (message == null)
            return $("body").loading({
                message: "Working..."
            });
        else
            return $("body").loading({
                message: message
            });
    },
    Stop: function () {
        return $("body").loading("stop");
    }
};
