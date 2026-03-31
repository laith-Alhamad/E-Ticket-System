import { useEffect, useState } from "react";
import axios from "axios";
import TicketList from "./components/TicketList";
import CheckoutForm from "./components/CheckoutForm";
import SuccessScreen from "./components/SuccessScreen";

function App() {
  const [tickets, setTickets] = useState([]);
  const [selectedTicket, setSelectedTicket] = useState(null);
  const [checkoutResult, setCheckoutResult] = useState(null);
  const [loading, setLoading] = useState(false);

  const API_BASE_URL = "https://localhost:44352/api";

  const fetchTickets = async () => {
    try {
      setLoading(true);
      const response = await axios.get(`${API_BASE_URL}/Tickets`);
      setTickets(response.data);
    } catch (error) {
      console.error("Error fetching tickets:", error);
      alert("Failed to load tickets");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchTickets();
  }, []);

  return (
    <div c style={{ padding: "30px", fontFamily: "Arial" }}>
      <h1>E-Ticketing Platform</h1>

      {loading && <p>Loading...</p>}

      {!loading && !checkoutResult && (
        <>
          <TicketList tickets={tickets} onSelectTicket={setSelectedTicket} />

          {selectedTicket && (
            <CheckoutForm
              ticket={selectedTicket}
              apiBaseUrl={API_BASE_URL}
              onSuccess={(result) => {
                setCheckoutResult(result);
                setSelectedTicket(null);
                fetchTickets();
              }}
            />
          )}
        </>
      )}

      {checkoutResult && (
        <SuccessScreen
          result={checkoutResult}
          onBack={() => setCheckoutResult(null)}
        />
      )}
    </div>
  );
}

export default App;