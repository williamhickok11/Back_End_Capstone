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
		let currentUser = AuthFactory.getUser();

		// Get access to the loged in user
		$http
			.get(`http://localhost:49881/api/Musician/${currentUser.IdMusician}`)
			.success(selectedUser => {
				$scope.currUser = selectedUser[0];
				console.log($scope.currUser);
			});

		$scope.saveChanges = function() {
			console.log($scope.currUser)

			console.log("go");
			$http({
		        url:`http://localhost:49881/api/Musician/${currentUser.IdMusician}`,
		        method: 'PUT',
		        data: JSON.stringify($scope.currUser)
		     })
		}

	}
]);


