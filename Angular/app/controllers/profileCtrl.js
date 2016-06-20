"use strict";

SwapNShop.controller("profileCtrl", [
  	"$scope",
  	"$http",
  	"$location",
  	"$routeParams",
  	"EquipFactory",
  	"AuthFactory",

  	function ($scope, $http, $location, $routeParams, EquipFactory, AuthFactory) {
		$scope.currUser = {};
		let currUser = AuthFactory.getUser();

		// Get access to the loged in user
		$http
			.get(`http://localhost:49881/api/Musician/${currUser.IdMusician}`)
			.success(selectedUser => {
				$scope.currUser = selectedUser[0];
				console.log($scope.currUser	);
			});

	}
]);


