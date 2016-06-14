"use strict";

MusicHistory.filter("reverse",
  () =>
    input => (input !== null) ? input.split("").reverse().join("") : ""
);
