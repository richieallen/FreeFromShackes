var app = angular.module('swagApp', ['ngRoute']);


app.run(['$rootScope', function ($rootScope) {
    $rootScope.baseUrl = document.querySelector('#swagApp').getAttribute('data-base-url');
}]);

app.service("dataService", [
        '$http', '$rootScope', function ($http, $rootScope) {
            if (typeof String.prototype.endsWith !== 'function') {
                String.prototype.endsWith = function (suffix) {
                    return this.indexOf(suffix, this.length - suffix.length) !== -1;
                };
            }

            if (typeof String.prototype.startsWith !== 'function') {
                String.prototype.startsWith = function (prefix) {
                    return this.indexOf(prefix) === 0;
                };
            }

            var getBaseUrl = function () {
                if ($rootScope.baseUrl.endsWith('/')) {
                    return $rootScope.baseUrl;
                }
                return $rootScope.baseUrl + "/";
            }

            var sanitiseRelativePath = function (relativePath) {
                if (relativePath.startsWith('/')) {
                    return relativePath.substring(1, relativePath.length);
                }
                return relativePath;
            }

            var getUrl = function (relativeUrl) {
                return getBaseUrl() + sanitiseRelativePath(relativeUrl);
            }

            return {
                getAll: function (datatype, onSuccess, onError) {
                    var url = getUrl(datatype);
                    console.log("GET: " + url);
                    $http.get(url).success(onSuccess).error(onError);
                },

                get: function (datatype, id, onSuccess, onError) {
                    var url = getUrl(datatype) + "/" + id;
                    console.log("GET: " + url);
                    $http.get(url).success(onSuccess).error(onError);
                },

                post: function (datatype, data, onSuccess, onError) {
                    var url = getUrl(datatype);
                    console.log("POST: " + url);
                    $http.post(url, data).success(onSuccess).error(onError);
                },

                put: function (datatype, data, onSuccess, onError) {
                    var url = getUrl(datatype);
                    console.log("PUT: " + url);
                    $http.put(url, data).success(onSuccess).error(onError);
                },

                delete: function (datatype, id, onSuccess, onError) {
                    var url = getUrl(datatype) + "/" + id;
                    console.log("DELETE: " + url);
                    $http.delete(url, id).success(onSuccess).error(onError);
                },

                getUrl: function (relativeUrl) {
                    return getUrl(relativeUrl);
                },

                getApiUrl: function (relativeUrl) {
                    return '/api/' + sanitiseRelativePath(relativeUrl);
                }
            }
        }
]);

app.factory('state', function ($rootScope) {
    'use strict';
    var state = {
        swagResult: {}
    };

    var broadcast = function (state) {
        $rootScope.$broadcast('state.update', state);
    };

    var update = function (newState) {
        state = newState;
        broadcast(state);
    };

    // in the service
    var onUpdate = function ($scope, callback) {
        $scope.$on('state.update', function (newState) {
            callback(newState);
        });
    };

    return {
        update: update,
        state: state,
        onUpdate: onUpdate
    };
});

app.controller('MainController', ['$scope', '$filter', 'dataService', function ($scope, $filter, dataService) {

    $scope.swagEm = function () {
        $scope.swagResult = null;
        return dataService.post('/swagEm/swagEm', null, function (result) {
            $scope.swagResult = result;
        }, function (result) {
            alert(result);
        });
    }
}]);