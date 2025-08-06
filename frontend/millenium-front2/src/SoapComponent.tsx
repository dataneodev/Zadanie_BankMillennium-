import React, { useEffect, useState } from "react";

const SoapComponent = () => {
  const [data, setData] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const soapBody = `
      <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:web="https://localhost:7162">
        <soapenv:Header/>
       <soap:Body>
        <Add xmlns=""https://localhost:7162/"">
          <x>2</x>
          <y>3</y>
        </Add>
      </soap:Body>
      </soapenv:Envelope>
    `;

    fetch("https://localhost:7162/FakeService", {
      method: "POST",
      headers: {
        "Content-Type": "text/xml;charset=UTF-8",
        SOAPAction: "https://localhost:7162/FakeService/Add",
      },
      body: soapBody,
    })
      .then((res) => {
        if (!res.ok) throw new Error("SOAP API error");
        return res.text();
      })
      .then((str) => {
        const parser = new DOMParser();
        const xmlDoc = parser.parseFromString(str, "text/xml");
        const result =
          xmlDoc.getElementsByTagName("GetExampleResponse")[0]?.textContent;
        setData(result || "Brak odpowiedzi");
      })
      .catch((err) => setError(err.message));
  }, []);

  if (error) return <p style={{ color: "red" }}>Błąd SOAP: {error}</p>;
  if (!data) return <p>Ładowanie SOAP danych...</p>;

  return (
    <div>
      <h2>Dane z SOAP API</h2>
      <pre>{data}</pre>
    </div>
  );
};

export default SoapComponent;
