'use client';
import React from "react";
import { Box, Typography } from "@mui/material";
import Link from "next/link";
const Footer = () => {
  return (
    <Box sx={{ pt: 6, textAlign: "center" }}>
      <Typography>
        © 2025 All rights reserved by PhuLaDeveloper
        <Link href="https://www.facebook.com/profile.php?id=100054133811363">
          PhucLaFacebook.com
        </Link>{" "}
      </Typography>
    </Box>
  );
};

export default Footer;
