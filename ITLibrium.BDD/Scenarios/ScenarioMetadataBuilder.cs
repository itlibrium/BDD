﻿using System;
using System.Collections.Generic;
using ITLibrium.Bdd.Reports;

namespace ITLibrium.Bdd.Scenarios
{
    internal class ScenarioMetadataBuilder : IScenarioMetadataBuilder
    {
        private string _testedComponent;
        
        private readonly string _title;

        private List<IBddReport> _reports;

        private bool _excludeFromReports;

        public ScenarioMetadataBuilder(string testedComponent, string title, bool excludeFromReports, IBddReport report)
        {
            if (excludeFromReports && report != null)
                throw new InvalidOperationException();

            _testedComponent = testedComponent;
            _title = title;

            if (report != null)
                ReportTo(report);
        }

        public IScenarioMetadataBuilder TestedComponent(string testedComponent)
        {
            _testedComponent = testedComponent;
            return this;
        }

        public IScenarioMetadataBuilder ReportTo(IBddReport report)
        {
            if (_excludeFromReports)
                throw new InvalidOperationException();

            if (_reports == null)
                _reports = new List<IBddReport>();

            _reports.Add(report);
            return this;
        }

        public IScenarioMetadataBuilder ExcludeFromReports()
        {
            if (_reports != null && _reports.Count > 0)
                throw new InvalidOperationException();

            _excludeFromReports = true;
            return this;
        }

        public IGivenBuilder<TContext> Using<TContext>() where TContext : class, new()
        {
            return new BddScenarioBuilder<TContext>(_testedComponent, _title, new TContext(), _excludeFromReports, _reports);
        }

        public IGivenBuilder<TContext> Using<TContext>(TContext context)
        {
            return new BddScenarioBuilder<TContext>(_testedComponent, _title, context, _excludeFromReports, _reports);
        }

        [Obsolete]
        public IGivenContinuationBuilder<TContext> Given<TContext>() where TContext : class, new()
        {
            return new BddScenarioBuilder<TContext>(_testedComponent, _title, new TContext(), _excludeFromReports, _reports);
        }

        [Obsolete]
        public IGivenContinuationBuilder<TContext> Given<TContext>(TContext fixture)
        {
            return new BddScenarioBuilder<TContext>(_testedComponent, _title, fixture, _excludeFromReports, _reports);
        }
    }
}