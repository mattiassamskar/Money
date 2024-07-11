import { BrowserRouter, Route } from "react-router-dom";
import { MainContainer } from "./MainContainer";
import { EditContainer } from "./EditContainer";

const App = () => (
  <BrowserRouter>
    <Route path="/" exact component={MainContainer} />
    <Route path="/edit" component={EditContainer} />
  </BrowserRouter>
);

export default App;
