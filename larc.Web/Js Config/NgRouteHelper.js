if (typeof (larc) == "undefined") {
    larc = {};
};

larc.Routes = {
    GetMainRoute: function () {
        return {
            templateUrl: "/Components/Base/Base.html",
            controller: "BaseController"
        };
    },
    GetHeaderRoute: function () {
        return {
            templateUrl: larc.Utilities.GetComponentHtmlTemplateUrl("Header", "Header"),
            controller: larc.Components.Header.Controller.Name
            //resolve: {
            //    currentUser: [
            //        "appService", function (appService) {
            //            return appService.GetData("Account/GetCurrentUser");
            //        }
            //    ]
            //}
        };
    },
    GetSideMenuRoute: function () {
        return {
            templateUrl: larc.Utilities.GetComponentHtmlTemplateUrl(larc.Components.SideMenu.Name, larc.Components.SideMenu.Name),
            controller: larc.Components.SideMenu.Controller.Name,
            resolve: {
            }
        };
    },
    SetRoutes: function (entityName) {
        switch (entityName) {
            case "Home":
                return {
                    name: "Home",
                    url: "/Home",
                    views: {
                        "": larc.Routes.GetMainRoute(),
                        "body@Home": {
                            templateUrl: "/Components/Home/Home.html",
                        }
                    }
                };
            case "Podcasts":
                return {
                    name: "Podcasts",
                    url: "/Podcasts",
                    views: {
                        "": larc.Routes.GetMainRoute(),
                        "body@Podcasts": {
                            templateUrl: "/Components/Podcast/Podcasts.html",
                            controller: "PodcastsController",
                            resolve: {
                                podcasts: [
                                    "appQueries", function (appQueries) {
                                        return appQueries.GetData("/Podcast/GetPodcasts");
                                    }
                                ]
                            }
                        }
                    }
                };
            case "Podcast":
                return {
                    name: "Podcast",
                    url: "/Podcast/?podcastid",
                    views: {
                        "": larc.Routes.GetMainRoute(),
                        "body@Podcast": {
                            templateUrl: "/Components/Podcast/Podcast.html",
                            controller: "PodcastController",
                            resolve: {
                                podcast: [
                                    "$stateParams", "appQueries", function ($stateParams, appQueries) {
                                        return $stateParams$stateParams["podcastid"];//appQueries.GetData("/Podcast/GetPodcast", { id: $stateParams["podcastid"] });
                                    }
                                ]
                            }
                        }
                    }
                };
            case "Bible":
                return {
                    name: "Bible",
                    url: "/Bible",
                    views: {
                        "": larc.Routes.GetMainRoute(),
                        "body@Bible": {
                            templateUrl: "/Components/Bible/Bible.html",
                        }
                    }
                };
            case "Events":
                return {
                    name: "Events",
                    url: "/Events",
                    views: {
                        "": larc.Routes.GetMainRoute(),
                        "body@Events": {
                            templateUrl: "/Components/Events/Events.html",
                            controller: "EventsController",
                            resolve: {
                                events: [
                                    "appQueries", function (appQueries) {
                                        return appQueries.GetData("/Event/GetEvents");
                                    }
                                ]
                            }
                        }
                    }
                };
            case "ContactUs":
                return {
                    name: "ContactUs",
                    url: "/ContactUs",
                    views: {
                        "": larc.Routes.GetMainRoute(),
                        "body@ContactUs": {
                            templateUrl: "/Components/ContactUs/ContactUs.html",
                        }
                    }
                };
            case "Gallery":
                return {
                    name: "Gallery",
                    url: "/Gallery",
                    views: {
                        "": larc.Routes.GetMainRoute(),
                        "body@Gallery": {
                            templateUrl: "/Components/Gallery/Gallery.html",
                            controller: "GalleryController",
                            resolve: {
                                pictures: [
                                    "appQueries", function (appQueries) {
                                        return appQueries.GetData("/Gallery/GetPhotos");
                                    }
                                ]
                            }
                        }
                    }
                };
            case "Admin":
                return {
                    name: "Admin",
                    url: "/Admin",
                    views: {
                        "": larc.Routes.GetMainRoute(),
                        "body@Admin": {
                            templateUrl: "/Components/Admin/Admin.html",
                        }
                    }
                };
        }
        return {};
    },
    SetLookupRecordListRoute: function (uibModal, templateUrl, controllerName, resolve, setSelectedObject) {
        uibModal.open({
            animation: true,
            ariaLabelledBy: "modal-title",
            ariaDescribedBy: "modal-body",
            templateUrl: templateUrl,
            controller: controllerName,
            size: "lg",
            resolve: resolve
        }).result.then(function (selectedRecord) {
            setSelectedObject(selectedRecord);
        });
    },
    SetAddLookup: function (uibModal, templateUrl, controllerName, onSuccess, resolve) {
        uibModal
            .open({
                animation: true,
                ariaLabelledBy: "modal-title",
                ariaDescribedBy: "modal-body",
                templateUrl: templateUrl,
                controller: controllerName,
                size: "lg",
                resolve: resolve
            })
            .result.then(function (selectedRecord) {
                onSuccess(selectedRecord);
            });
    },
    SetEntityViewLookup: function (uibModal, templateUrl, controllerName, resolve) {
        uibModal.open({
            animation: true,
            ariaLabelledBy: "modal-title",
            ariaDescribedBy: "modal-body",
            templateUrl: templateUrl,
            controller: controllerName,
            size: "lg",
            resolve: resolve
        });
    },
    ConfirmationLookup: function (uibModal, successFunction) {
        uibModal
            .open({
                animation: true,
                ariaLabelledBy: "modal-title",
                ariaDescribedBy: "modal-body",
                templateUrl: "/components/shared/confirmationdialog/confirmation-dialog.html",
                controller: "ModalConfirmationDialogDoctorController",
                size: "lg"
            })
            .result.then(function (result) {
                if (result === "ok")
                    successFunction();
            });
    }
};