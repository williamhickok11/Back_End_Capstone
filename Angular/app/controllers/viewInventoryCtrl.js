"use strict";

SwapNShop.controller("viewInventoryCtrl", [
  "$scope",
  "$http",
  "$location",
  "AuthFactory",

  function ($scope, $http, $location, AuthFactory) {
  	$scope.equipment = [];
  	let user = AuthFactory.getUser();
  	console.log('user', user.IdMusician)
  	var curr_musician_ID = parseInt(user.IdMusician);
		$http
			.get(`http://localhost:49881/api/Equipment?M_ID=${curr_musician_ID}`)
			.success(inv => {
				$scope.equipment = inv;
				console.log("equipment", $scope.equipment);
			});
			
	}
]);
