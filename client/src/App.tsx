import { BrowserRouter, Route } from "react-router-dom";
import { MainContainer } from "./MainContainer";
import { EditContainer } from "./EditContainer";
import moment from "moment";
import "moment/locale/sv";

moment.locale("sv");

const App = () => (
  <BrowserRouter>
    <Route path="/" exact component={MainContainer} />
    <Route path="/edit" component={EditContainer} />
  </BrowserRouter>
);

export default App;
