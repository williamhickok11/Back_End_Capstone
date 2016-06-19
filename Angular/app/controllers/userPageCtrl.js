"use strict";

SwapNShop.controller("userPageCtrl", [
  	"$scope",
  	"$http",
  	"$location",
  	"$routeParams"
  	"EquipFactory",
  	"AuthFactory",

  	function ($scope, $http, $location, $routeParams, EquipFactory, AuthFactory) {
		let currUser = AuthFactory.getUser();
      	let currSelectedUserId = SelectedUserFactory.getUserId();
		$scope.selectedUser;
		console.log("currUser", currUser)
		console.log("selsectedUser", currSelectedUserId)


		// Get access to the user that has been clicked on
		$http
			.get(`http://localhost:49881/api/Musician/${currSelectedUserId}`)
			.success(selectedUser => {
				$scope.selectedUser = selectedUser
				console.log($scope.selectedUser)
			});

	}
]);


