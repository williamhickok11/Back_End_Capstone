"use strict";
 console.log("got here")

SwapNShop.controller("mainCtrl", [
  "$scope",
  "$http",

  function ($scope, $http) {
  	console.log("go")
    $scope.figurines = [];

    $http.get('http://localhost:5000/api/Inventory')
    .success(results => $scope.figurines = results);
    }
]);
