"use strict";

SwapNShop.controller("profileCtrl", [
  	"$scope",
  	"$http",
  	"$location",
  	"$routeParams",
  	"EquipFactory",
  	"AuthFactory",
  	"SelectedUserFactory",

  	function ($scope, $http, $location, $routeParams, EquipFactory, AuthFactory) {
		$scope.currUser = {};
		let currMusician = AuthFactory.getUser();
		$scope.allComments = [];

		// View the page of the user you clicked on
	    $scope.goToPerson = function (id) {
	      	SelectedUserFactory.setUserId(id)
	      	$location.path("/user_page");
	    }

		// Get access to the loged in user
		$http
			.get(`http://localhost:49881/api/Musician/${currMusician.IdMusician}`)
			.success(selectedUser => {
				$scope.currUser = selectedUser[0];
				console.log($scope.currUser);
			})
			.then(function(){
				$http
				.get(`http://localhost:49881/api/Comments/${currMusician.IdMusician}`)
				.success(comments => {
					$scope.allComments = comments;
					// refactor the dates
					for (var i = 0; i < $scope.allComments.length; i++) {
						var date = $scope.allComments[i].date.split(/\-|\T/);
						$scope.allComments[i].date = date[1] + "/" + date[2] + "/" + date[0];
					}
					console.log("allComments", $scope.allComments)
				})
			})

		$scope.saveChanges = function() {
			console.log($scope.currUser)

			console.log("go");
			$http({
		        url:`http://localhost:49881/api/Musician/${currMusician.IdMusician}`,
		        method: 'PUT',
		        data: JSON.stringify($scope.currUser)
		     })

		}

	}
]);


