"use strict";

/* exported MusicHistory */

let SwapNShop = angular.module("SwapNShop", ["ngRoute"])

SwapNShop.config(["$routeProvider",
  function ($routeProvider) {
    $routeProvider.
      when("/", {
        templateUrl: "partials/register.html",
        controller: 'registerCtrl'
      }).
      when("/homepage", {
        templateUrl: "partials/homepage.html",
        controller: 'mainCtrl'
      }).
      when("/add_equipment", {
        templateUrl: "partials/add_equipment.html",
        controller: 'addEquipmentCtrl'
      }).
      otherwise({
        redirectTo: "/"
      });
  }]);

