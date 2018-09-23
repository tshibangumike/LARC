angular.module("larcApp")
    .service("appQueries",
    [
        "$http", "$q", "$state",
        function ($http, $q, $state) {
            return {
                GetData: function (url, parameters) {
                    return $http({
                        method: "POST",
                        url: url,
                        data: parameters
                    });
                },
                PostForm: function (url, parameters) {
                    return $http({
                        method: "POST",
                        url: url,
                        data: $.param(parameters),
                        headers: { 'Content-Type': "application/x-www-form-urlencoded" }
                    });
                },
                NavigateTo: function (stateName, parameters) {
                    if (parameters != null) {
                        $state.go(stateName, parameters);
                    } else {
                        $state.go(stateName);
                    }
                },
                UploadFile: function (url, documents, paramObject, inputIdName) {
                    if (paramObject == null) return $http.get(url);
                    var parameterUrl = "";
                    var count = 0;
                    var formData = new FormData();
                    _.forEach(paramObject, function (value, key) {
                        formData.append(key, value);
                    });
                    for (var i = 0; i < documents.length; i++) {
                        formData.append(inputIdName, documents[i]);
                    }
                    return $http.post(url, formData, {
                        withCredentials: true,
                        headers: { 'Content-Type': undefined },
                        transformRequest: angular.identity
                    });
                },
                RefreshCurrentState: function () {
                    $state.go($state.current, {}, { reload: true });
                },
                GetCurrentState: function () {
                    return $state.current;
                },
                Test: function (parameter) {
                    $state.go('Teaching', { coords: { x: 10399.2, y: 49071 } });
                }
            };
        }
    ]);
