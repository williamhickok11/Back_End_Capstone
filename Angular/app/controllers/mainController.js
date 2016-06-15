"use strict";

SwapNShop.controller("mainCtrl", [
  "$scope",
  "$http",
  "$location",

  function ($scope, $http, $location) {
  	$scope.equipment = [];

		$http
			.get('http://localhost:49881/api/Equipment')
			.success(inv => {
				$scope.equipment = inv;
				console.log("equipment", $scope.equipment);
			});
			
	}
]);
