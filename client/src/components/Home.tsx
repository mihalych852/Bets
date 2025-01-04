import React, { useState, useEffect } from "react";

import { getPublicContent } from "../services/user.service";
import PublicContent from "./PublicContent";
import IndexBets from "../pages/IndexBets";

//This is a public page that shows public content. People don’t need to log in to view this page.
const Home: React.FC = () => {
  return (
    <IndexBets />
  );
};

export default Home;