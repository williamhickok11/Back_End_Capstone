"use strict";

MusicHistory.filter("sort",
  () =>
    input => (input !== null) ? input.split("").sort().join("") : ""
);
