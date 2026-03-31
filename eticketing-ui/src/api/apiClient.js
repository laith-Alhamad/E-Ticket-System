import axios from "axios";

const api = axios.create({
  baseURL: "[localhost](https://localhost:44352/api)", // ✅ includes /api
});

export const getTickets = async () => {
  const res = await api.get("/tickets");
  return res.data; // should be an array
};
