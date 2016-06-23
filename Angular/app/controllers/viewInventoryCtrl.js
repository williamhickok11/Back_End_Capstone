"use strict";

SwapNShop.controller("viewInventoryCtrl", [
  "$scope",
  "$http",
  "$location",
  "$route",
  "AuthFactory",
  "SelectedUserFactory",

  function ($scope, $http, $location, $route, AuthFactory, SelectedUserFactory) {
    let user = AuthFactory.getUser();
    console.log(user)
    $scope.equipment = [];
    $scope.selectedEquipment = {};
    $scope.rentalDates = [];
    console.log('user', user.IdMusician)
    var curr_musician_ID = parseInt(user.IdMusician);

    if (user === null){
      $location.path("#/login");
    }

    //Populate the modal
    $scope.showModal = function(id){
      console.log("show modal");
      console.log("id", id);
      //Get all the requested rental dates for this particular piece of equipment
      $http
      .get(`http://localhost:49881/api/RentalDates?EquipmentID=${id}&confirmCheck=false`)
      .success(dates => {
        $scope.rentalDates = dates;
        console.log("rental Dates", $scope.rentalDates)
        //Split all the dates into readable dates
        for (var i = 0; i < $scope.rentalDates.length; i++) {
          var checkInDates = $scope.rentalDates[i].checkInDates.split(/\-|\T/)
          $scope.rentalDates[i].checkInDates = checkInDates[1] + "/" + checkInDates[2] + "/" + checkInDates[0];
          var checkOutDates = $scope.rentalDates[i].checkOutDates.split(/\-|\T/)
          $scope.rentalDates[i].checkOutDates = checkOutDates[1] + "/" + checkOutDates[2] + "/" + checkOutDates[0];
        }
        //Display the modal
        $('.modal-trigger').leanModal();
      });

      // Get the information for the equipment that the user clicked on
      $http
      .get(`http://localhost:49881/api/Equipment?EquipmentID=${id}`)
      .success(curEqu => {
        $scope.selectedEquipment = curEqu[0];
        console.log('selectedEquipment', $scope.selectedEquipment)
      });
    }
  	
    // Get the equipment for the user that is loged in
		$http
			.get(`http://localhost:49881/api/Equipment?MusicianID=${curr_musician_ID}`)
			.success(inv => {
				$scope.equipment = inv;
				console.log("equipment", $scope.equipment);
			});

    // View the page of the user you clicked on
    $scope.goToPerson = function (id) {
      SelectedUserFactory.setUserId(id)
      if (user.IdMusician == id){
        $location.path("/profile");
      } else {
        $location.path("/user_page");
      }
    }

    let deleteRentalRequestNote = function(recievingMusicianId) {
      console.log("recievingMusicianId", recievingMusicianId)
      $http({
        url:`http://localhost:49881/api/Notification/${recievingMusicianId}/true`,
        method: 'DELETE',
      })
    }

    $scope.respontToRental = function (confirm, id, recievingMusicianId) {
      let itemToDelete = this.person;
      $scope.rentalDates.splice($scope.rentalDates.indexOf(itemToDelete), 1);
      // Post that the equipment has been responded to
      $http({
        url:`http://localhost:49881/api/Equipment/${$scope.selectedEquipment.id}/-1`,
        method: 'PUT',
        // data: JSON.stringify($scope.equipment)
      })
      .success(function newEquipment (){
        console.log('201 updated')
        if (confirm === true) {
          //Update the rental request database object to be confirmed=true
          $http({
              url:`http://localhost:49881/api/RentalDates/${id}`,
              method: 'PUT',
            })
          //Notify the rent request user that you have confirmed the rental
          let notificationCreation = {
              IdPostingMusician : user.IdMusician,
              IdRecievingMusician : recievingMusicianId,
              description : `${user.userName} has accepted your rental request`,
            }
            // Post a deny request notification
            $http({
              url:'http://localhost:49881/api/Notification',
              method: 'POST',
              data: JSON.stringify(notificationCreation)
            })
            .then(function() {
              //Remove the rental requst notification
              deleteRentalRequestNote(recievingMusicianId);
            })

        } else {
          //Notify the rent request user that you have denied the rental
          console.log("DELETE ID", id)
          //Delete the request
          $http({
            url:`http://localhost:49881/api/RentalDates/${id}`,
            method: 'DELETE',
          })
          .success(function postNotification (){
            let notificationCreation = {
              IdPostingMusician : user.IdMusician,
              IdRecievingMusician : recievingMusicianId,
              description : `${user.userName} has denied your rental request`,
            }
            // Post a deny request notification
            $http({
              url:'http://localhost:49881/api/Notification',
              method: 'POST',
              data: JSON.stringify(notificationCreation)
            })
            .then(function() {
              //Remove the rental requst notification
              deleteRentalRequestNote(recievingMusicianId);
            })
          })
        }
      })
      $location.path("/notifications");
    }
	}
]);
