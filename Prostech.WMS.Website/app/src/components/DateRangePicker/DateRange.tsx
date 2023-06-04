import React from "react";
import "../styles.css";

import { DateRangePicker } from "rsuite";

const DateRange = () => (
  <>
    <DateRangePicker
      appearance="default"
      placeholder="Choose Date Range"
      style={{ width: 230 }}
    />
  </>
);

export default DateRange;
